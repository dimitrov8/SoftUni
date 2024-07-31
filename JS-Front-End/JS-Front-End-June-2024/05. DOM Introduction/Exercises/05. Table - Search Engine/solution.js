function solve() {
  document.querySelector('#searchBtn').addEventListener('click', onClick);

  function onClick() {
    const cellElements = document.querySelectorAll('.container tbody td');
    const query = getSearchQuery();
    resetStylesAndClearSearch(cellElements);
    highlightMatches(cellElements, query);
  }

  function getSearchQuery() {
    const searchInput = document.getElementById('searchField');
    return searchInput.value.trim().toLowerCase();
  }

  function resetStylesAndClearInput(cellElements) {
    cellElements.forEach((cell) => {
      cell.parentElement.classList.remove('select');
    });
    document.getElementById('searchField').value = '';
  }

  function highlightMatches(cellElements, searchText) {
    cellElements.forEach((cell) => {
      const cellText = cell.textContent.trim().toLowerCase();
      if (cellText.includes(searchText) && searchText !== '') {
        cell.parentElement.classList.add('select');
        return;
      }
    });
  }
}
