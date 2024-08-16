const baseUrl = `http://localhost:3030/jsonstore/records`;

let currentRecordId = null;

const loadRecordsBtn = document.getElementById('load-records');
const addRecordBtn = document.getElementById('add-record');
const editRecordBtn = document.getElementById('edit-record');
const recordsList = document.getElementById('list');
const recordNameInput = document.getElementById('p-name');
const recordStepsInput = document.getElementById('steps');
const recordCaloriesInput = document.getElementById('calories');

loadRecordsBtn.addEventListener('click', loadRecords);
addRecordBtn.addEventListener('click', handleAddRecord);
editRecordBtn.addEventListener('click', handleEditRecord);

async function loadRecords() {
  const response = await fetch(baseUrl);
  const records = await response.json();
  recordsList.innerHTML = '';

  console.log('Records Loaded:');
  Object.values(records).forEach((record) => {
    console.log(record);

    const recordElement = createRecordElement(
      record.name,
      record.steps,
      record.calories,
      record._id
    );

    recordsList.append(recordElement);
  });
}

function createRecordElement(name, steps, calories, id) {
  const pNameElement = document.createElement('p');
  pNameElement.textContent = name;

  const pStepsElement = document.createElement('p');
  pStepsElement.textContent = steps;

  const pCaloriesElement = document.createElement('p');
  pCaloriesElement.textContent = calories;

  const divInfo = document.createElement('div');
  divInfo.classList.add('info');
  divInfo.append(pNameElement, pStepsElement, pCaloriesElement);

  const changeBtnElement = document.createElement('button');
  changeBtnElement.textContent = 'Change';
  changeBtnElement.dataset.recordId = id;
  changeBtnElement.classList.add('change-btn');

  const deleteBtnElement = document.createElement('button');
  deleteBtnElement.textContent = 'Delete';
  deleteBtnElement.dataset.recordId = id;
  deleteBtnElement.classList.add('delete-btn');

  const divBtnWrapper = document.createElement('div');
  divBtnWrapper.classList.add('btn-wrapper');
  divBtnWrapper.append(changeBtnElement, deleteBtnElement);

  const liRecordElement = document.createElement('li');
  liRecordElement.classList.add('record');
  liRecordElement.append(divInfo, divBtnWrapper);

  changeBtnElement.addEventListener('click', () => {
    if (currentRecordId == null) {
      currentRecordId = id;

      recordNameInput.value = name;
      recordStepsInput.value = steps;
      recordCaloriesInput.value = calories;

      addRecordBtn.setAttribute('disabled', true);
      editRecordBtn.removeAttribute('disabled');

      liRecordElement.remove();
    }
  });

  deleteBtnElement.addEventListener('click', () => {
    handleDelete(id);
  });

  return liRecordElement;
}

async function handleAddRecord() {
  if (isValidInput()) {
    const recordData = {
      name: recordNameInput.value,
      steps: recordStepsInput.value,
      calories: recordCaloriesInput.value,
    };

    await fetch(baseUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(recordData),
    });

    clearInputs();
    loadRecords();
  }
}

async function handleEditRecord() {
  if (currentRecordId && isValidInput()) {
    await fetch(`${baseUrl}/${currentRecordId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: recordNameInput.value,
        steps: recordStepsInput.value,
        calories: recordCaloriesInput.value,
        _id: currentRecordId,
      }),
    });

    clearInputs();
    loadRecords();
    editRecordBtn.setAttribute('disabled', true);
    addRecordBtn.removeAttribute('disabled');

    currentRecordId = null;
  }
}

async function handleDelete(id) {
  await fetch(`${baseUrl}/${id}`, {
    method: 'DELETE',
  });

  await loadRecords();
}

function isValidInput() {
  return (
    recordNameInput.value !== '' &&
    recordStepsInput.value !== '' &&
    recordCaloriesInput.value !== ''
  );
}

function clearInputs() {
  recordNameInput.value = '';
  recordStepsInput.value = '';
  recordCaloriesInput.value = '';
}
