const baseUrl = `http://localhost:3030/jsonstore/games`;

let currentGameId = null;

const loadButton = document.getElementById('load-games');
const gamesList = document.getElementById('games-list');
const addButton = document.getElementById('add-game');
const editButton = document.getElementById('edit-game');
const gameNameInput = document.getElementById('g-name');
const typeInput = document.getElementById('type');
const maxPlayersInput = document.getElementById('players');

loadButton.addEventListener('click', loadGames);
addButton.addEventListener('click', handleAddGame);
editButton.addEventListener('click', handleEditGame);

async function loadGames() {
  const response = await fetch(baseUrl);
  const games = await response.json();
  gamesList.innerHTML = '';

  console.log('Games Loaded:');
  Object.values(games).forEach((game) => {
    console.log(game);

    const gameElement = createGameElement(
      game.name,
      game.type,
      game.players,
      game._id
    );

    gamesList.append(gameElement);
  });
}

function createGameElement(name, type, players, gameId) {
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
  changeBtnElement.dataset.gameId = gameId;
  changeBtnElement.textContent = 'Change';

  const deleteBtnElement = document.createElement('button');
  deleteBtnElement.classList.add('delete-btn');
  deleteBtnElement.dataset.gameId = gameId;
  deleteBtnElement.textContent = 'Delete';
  deleteBtnElement.addEventListener('click', async () => {
    await handleDelete(gameId);
  });

  const divButtonsContainerElement = document.createElement('div');
  divButtonsContainerElement.classList.add('buttons-container');
  divButtonsContainerElement.append(changeBtnElement, deleteBtnElement);

  const divBoardGameElement = document.createElement('div');
  divBoardGameElement.classList.add('board-game');
  divBoardGameElement.append(divContentElement, divButtonsContainerElement);

  changeBtnElement.addEventListener('click', () => {
    if (currentGameId == null) {
      currentGameId = gameId;

      gameNameInput.value = name;
      typeInput.value = type;
      maxPlayersInput.value = players;

      addButton.setAttribute('disabled', true);
      editButton.removeAttribute('disabled');

      divBoardGameElement.remove();
    }
  });

  return divBoardGameElement;
}

async function handleAddGame() {
  if (isValidInput()) {
    const gameData = {
      name: gameNameInput.value,
      type: typeInput.value,
      players: maxPlayersInput.value,
    };

    await fetch(baseUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(gameData),
    });

    clearInputs();
    await loadGames();
  }
}

async function handleEditGame() {
  if (currentGameId && isValidInput()) {
    await fetch(`${baseUrl}/${currentGameId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: gameNameInput.value,
        type: typeInput.value,
        players: maxPlayersInput.value,
        _id: currentGameId,
      }),
    });

    clearInputs();
    await loadGames();
    editButton.setAttribute('disabled', true);
    addButton.removeAttribute('disabled');
    currentGameId = null;
  }
}

function clearInputs() {
  gameNameInput.value = '';
  typeInput.value = '';
  maxPlayersInput.value = '';
}

async function handleDelete(gameId) {
  await fetch(`${baseUrl}/${gameId}`, {
    method: 'DELETE',
  });

  await loadGames();
}

function isValidInput() {
  return (
    gameNameInput.value !== '' &&
    typeInput.value !== '' &&
    maxPlayersInput.value !== ''
  );
}
