function addItem() {
  const newItemText = document.getElementById('newItemText').value.trim();

  if (newItemText == '') {
    return;
  }

  const items = document.getElementById('items');
  items.appendChild(createListItem(newItemText));

  document.getElementById('newItemText').value = '';

  // Function to create a list item with a delete link
  function createListItem(text) {
    const li = document.createElement('li');
    li.textContent = text + ' ';

    const deleteLink = document.createElement('a');
    deleteLink.href = '#';
    deleteLink.textContent = '[Delete]';
    deleteLink.onclick = (e) => {
      e.preventDefault();
      li.remove();
    };

    li.appendChild(deleteLink);
    return li;
  }
}
