const apiUrl = 'https://localhost:7157/api/film';  // Правильный путь к API
let editFilmId = null;  // Для хранения ID фильма при редактировании

// Функция загрузки списка фильмов
function loadFilms() {
    fetch(apiUrl)  // Запрос на получение всех фильмов
        .then(response => response.json())  // Преобразуем ответ в JSON
        .then(films => {
            const filmList = document.getElementById('film-list');
            filmList.innerHTML = '';  // Очищаем контейнер перед загрузкой новых данных

            // Для каждого фильма создаем карточку
            films.forEach(film => {
                const filmCard = document.createElement('div');
                filmCard.classList.add('film-card');
                filmCard.innerHTML = `
                    <h3>${film.title}</h3>
                    <p>Рейтинг: ${film.raiting}</p>
                    <p>Год: ${film.year}</p>
                    <button onclick="editFilm(${film.id}, '${film.title}', '${film.raiting}', ${film.year})">Редактировать</button>
                    <button onclick="deleteFilm(${film.id})">Удалить</button>
                    <button onclick="window.location.href='https://vk.com/video-114085305_456246533'">Смотреть</button>

                `;
                filmList.appendChild(filmCard);
            });
        })
        .catch(error => console.error('Error loading films:', error));  // Обрабатываем ошибки
}

// Функция отправки фильма (добавление или редактирование)
function submitFilm() {
    const title = document.getElementById('title').value;
    const raiting = document.getElementById('raiting').value;
    const year = document.getElementById('year').value;

    const filmData = {
        title: title,
        raiting: raiting,
        year: parseInt(year)
    };

    // Если редактируем фильм
    if (editFilmId) {
        fetch(`${apiUrl}/${editFilmId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(filmData)
        })
            .then(response => response.json())
            .then(() => {
                loadFilms();  // Перезагружаем список фильмов
                resetForm();  // Сбрасываем форму
                document.getElementById('submit-button').textContent = "Добавить фильм";  // Меняем кнопку на "Добавить"
            })
            .catch(error => console.error('Error updating film:', error));
    } else {
        // Если добавляем новый фильм
        fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(filmData)
        })
            .then(response => response.json())
            .then(() => {
                loadFilms();  // Перезагружаем список фильмов
                resetForm();  // Сбрасываем форму
            })
            .catch(error => console.error('Error adding film:', error));
    }
}

// Функция для редактирования фильма
function editFilm(id, title, raiting, year) {
    document.getElementById('title').value = title;
    document.getElementById('raiting').value = raiting;
    document.getElementById('year').value = year;
    editFilmId = id;

    document.getElementById('submit-button').textContent = "Редактировать фильм";
}

// Функция для удаления фильма
function deleteFilm(id) {
    fetch(`${apiUrl}/${id}`, {
        method: 'DELETE'
    })
        .then(() => loadFilms())  // Перезагружаем список фильмов после удаления
        .catch(error => console.error('Error deleting film:', error));
}

// Сброс формы
function resetForm() {
    document.getElementById('title').value = '';
    document.getElementById('raiting').value = '';
    document.getElementById('year').value = '';
    editFilmId = null;
}

// Загружаем фильмы при первой загрузке страницы
loadFilms();
