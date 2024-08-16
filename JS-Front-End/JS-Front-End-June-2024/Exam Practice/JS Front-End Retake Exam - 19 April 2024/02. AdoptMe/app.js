window.addEventListener('load', solve);

function solve() {
  // Define the main object for holding references and state
  adoptMe = {
    inputFields: [
      document.getElementById('type'),
      document.getElementById('age'),
      document.getElementById('gender'),
    ],
    form: document.querySelector('form'),
    adoptBtn: document.getElementById('adopt-btn'),
    adoptionList: document.getElementById('adoption-info'),
    adoptedList: document.getElementById('adopted-list'),
  };

  // Event listener for the "Adopt" button
  adoptMe.adoptBtn.addEventListener('click', handleAdd);

  // Handles the "Add" button click event
  function handleAdd(event) {
    event.preventDefault(); // Prevents the form from submitting

    // Validate input fields
    if (isValidInput(adoptMe.inputFields)) {
      const { type, age, gender } = getInputValues(); // Get values from inputs

      // Create and append the list item with buttons
      const liElement = createListItem(type, age, gender);
      adoptMe.adoptionList.appendChild(liElement);

      // Reset the form fields
      adoptMe.form.reset();
    }
  }

  // Creates a list item element for the adoption list
  function createListItem(type, age, gender) {
    // Create article element with pet details
    const articleElement = createArticleElement(type, age, gender);

    // Create and return button elements
    const { editBtnElement, doneBtnElement, divButtons } =
      createButtonElements();

    // Attach event listeners to buttons
    editBtnElement.addEventListener('click', handleEdit);
    doneBtnElement.addEventListener('click', handleDone);

    const liElement = document.createElement('li');
    liElement.appendChild(articleElement);
    liElement.appendChild(divButtons);

    return liElement;
  }

  // Creates an article element with pet details
  function createArticleElement(type, age, gender) {
    const pTypeElement = document.createElement('p');
    pTypeElement.textContent = `Pet:${type}`;

    const pGenderElement = document.createElement('p');
    pGenderElement.textContent = `Gender:${gender}`;

    const pAgeElement = document.createElement('p');
    pAgeElement.textContent = `Age:${age}`;

    const articleElement = document.createElement('article');
    articleElement.appendChild(pTypeElement);
    articleElement.appendChild(pGenderElement);
    articleElement.appendChild(pAgeElement);

    return articleElement;
  }

  // Creates button elements for list items
  function createButtonElements() {
    const editBtnElement = document.createElement('button');
    editBtnElement.textContent = 'Edit';
    editBtnElement.classList.add('edit-btn');

    const doneBtnElement = document.createElement('button');
    doneBtnElement.textContent = 'Done';
    doneBtnElement.classList.add('done-btn');

    const divButtons = document.createElement('div');
    divButtons.classList.add('buttons');
    divButtons.appendChild(editBtnElement);
    divButtons.appendChild(doneBtnElement);

    return { editBtnElement, doneBtnElement, divButtons };
  }

  // Creates a "Clear" button
  function createClearButton() {
    const clearButton = document.createElement('button');
    clearButton.textContent = 'Clear';
    clearButton.classList.add('clear-btn');

    // Removes the list item when "Clear" is clicked
    clearButton.addEventListener('click', function () {
      this.parentElement.remove();
    });

    return clearButton;
  }

  // Handles the "Edit" button click event
  function handleEdit() {
    const liElement = this.parentElement.parentElement; // Get the list item element
    const [type, gender, age] = getItemDetails(liElement); // Get details from list item

    // Set values back to input fields
    adoptMe.inputFields[0].value = type;
    adoptMe.inputFields[1].value = age;
    adoptMe.inputFields[2].value = gender;

    // Remove the list item
    liElement.remove();
  }

  // Handles the "Done" button click event
  function handleDone() {
    const liElement = this.parentElement.parentElement; // Get the list item element
    const divButtons = liElement.querySelector('.buttons'); // Get the button container

    // Remove the buttons container from the list item
    divButtons.remove();

    // Create and add the "Clear" button to the list item
    const clearButton = createClearButton();
    liElement.appendChild(clearButton);

    // Move the list item to the adopted list
    adoptMe.adoptedList.appendChild(liElement);
  }

  // Extracts item details from a list item element
  function getItemDetails(liElement) {
    const [type, age, gender] = Array.from(liElement.querySelectorAll('p')).map(
      (p) => p.textContent.split(':')[1].trim()
    );

    return [type, age, gender];
  }

  // Checks if all input fields are filled
  function isValidInput(inputs) {
    return inputs.every((input) => input.value.trim() !== '');
  }

  // Gets values from input fields
  function getInputValues() {
    const [type, age, gender] = adoptMe.inputFields.map((input) => input.value);

    return { type, age, gender };
  }
}
