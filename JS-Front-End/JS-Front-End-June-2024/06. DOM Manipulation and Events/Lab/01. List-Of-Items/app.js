function addItem() {
  const items = document.getElementById('items');
  const newItemInput = document.getElementById('newItemText');

  const newItemElement = document.createElement('li');
  newItemElement.textContent = newItemInput.value;

  items.appendChild(newItemElement);
  newItemInput.value = '';
}
