const baseUrl = `http://localhost:3030/jsonstore/games`;

const loadButton = document.getElementById('load-games');
const gamesList = document.getElementById('games-list');

loadButton.addEventListener('click', loadGames);

async function loadGames() {
  gamesList.innerHTML = '';

  const response = await fetch(baseUrl);
  const result = await response.json();
  const games = Object.values(result);

  const gameElements = games.map((game) =>
    createGameElement(game.name, game.type, game.players)
  );

  gamesList.append(...gameElements);
}

function createGameElement(name, type, players) {
  const pNameElement = document.createElement('p');
  pNameElement.textContent = name;

  const pTypeElement = document.createElement('p');
  pTypeElement.textContent = type;

  const pPlayersElement = document.createElement('p');
  pPlayersElement.textContent = players;

  const divContentElement = document.createElement('div');
  divContentElement.classList.add('content');
  divContentElement.append(pNameElement, pPlayersElement, pTypeElement);

  const changeBtnElement = document.createElement('button');
  changeBtnElement.classList.add('change-btn');
  changeBtnElement.textContent = 'Change';

  const deleteBtnElement = document.createElement('button');
  deleteBtnElement.classList.add('delete-btn');
  deleteBtnElement.textContent = 'Delete';

  const divButtonsContainerElement = document.createElement('div');
  divButtonsContainerElement.classList.add('buttons-container');
  divButtonsContainerElement.append(changeBtnElement, deleteBtnElement);

  const divBoardGameElement = document.createElement('div');
  divBoardGameElement.classList.add('board-game');
  divBoardGameElement.append(divContentElement, divButtonsContainerElement);

  return divBoardGameElement;
}
