function toggle() {
  const buttonElement = document.querySelector('span.button');
  const extraElement = document.getElementById('extra');

  buttonElement.textContent =
    buttonElement.textContent === 'More' ? 'Less' : 'More';

  extraElement.style.display =
    buttonElement.textContent === 'More' ? 'none' : 'block';
}
