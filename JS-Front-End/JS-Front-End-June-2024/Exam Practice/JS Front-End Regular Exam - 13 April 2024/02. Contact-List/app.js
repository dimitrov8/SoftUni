window.addEventListener('load', solve);

function solve() {
  const nameInput = document.getElementById('name');
  const phoneNumberInput = document.getElementById('phone');
  const categoryInput = document.getElementById('category');
  const checkList = document.getElementById('check-list');
  const addButton = document.getElementById('add-btn');
  const contactList = document.getElementById('contact-list');

  addEventListeners();

  function addEventListeners() {
    addButton.addEventListener('click', add);
  }

  function add() {
    if (
      nameInput.value == '' ||
      phoneNumberInput.value == '' ||
      categoryInput.value == ''
    ) {
      return;
    }

    createListItem(
      nameInput.value,
      phoneNumberInput.value,
      categoryInput.value
    );

    nameInput.value = '';
    phoneNumberInput.value = '';
    categoryInput.value = '';
  }

  function edit(name, phone, category, liElement) {
    nameInput.value = name;
    phoneNumberInput.value = phone;
    categoryInput.value = category;

    liElement.remove();
  }

  function save(liElement) {
    liElement.children[1].remove();

    const delButton = document.createElement('button');
    delButton.classList.add('del-btn');

    delButton.addEventListener('click', function () {
      remove(liElement);
    });

    liElement.appendChild(delButton);

    contactList.appendChild(liElement);
  }

  function remove(liElement) {
    liElement.remove();
  }

  function createListItem(name, phone, category) {
    const pNameElement = document.createElement('p');
    pNameElement.textContent = `name:${name}`;

    const pPhoneElement = document.createElement('p');
    pPhoneElement.textContent = `phone:${phone}`;

    const pCategoryElement = document.createElement('p');
    pCategoryElement.textContent = `category:${category}`;

    const articleElement = document.createElement('article');
    articleElement.appendChild(pNameElement);
    articleElement.appendChild(pPhoneElement);
    articleElement.appendChild(pCategoryElement);

    const divButtons = document.createElement('div');
    divButtons.classList.add('buttons');

    const editBtn = document.createElement('button');
    editBtn.classList.add('edit-btn');

    const saveBtn = document.createElement('button');
    saveBtn.classList.add('save-btn');

    divButtons.appendChild(editBtn);
    divButtons.appendChild(saveBtn);

    const liElement = document.createElement('li');
    liElement.appendChild(articleElement);
    liElement.appendChild(divButtons);

    checkList.appendChild(liElement);

    editBtn.addEventListener('click', function () {
      edit(name, phone, category, liElement);
    });

    saveBtn.addEventListener('click', function () {
      save(liElement);
    });
  }
}
