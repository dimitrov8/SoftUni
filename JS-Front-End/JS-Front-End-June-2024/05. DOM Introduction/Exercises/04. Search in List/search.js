function search() {
  const townElements = document.querySelectorAll('#towns li');
  const query = getSearchQuery();
  resetStyles(townElements);
  const matches = highlightMatches(townElements, query);
  showMatchCount(matches);

  function getSearchQuery() {
    const searchInput = document.getElementById('searchText');
    return searchInput.value.trim().toLowerCase();
  }

  function resetStyles(townElements) {
    townElements.forEach((town) => {
      town.style.fontWeight = 'normal';
      town.style.textDecoration = 'none';
    });
  }

  function highlightMatches(townElements, searchText) {
    let matchCount = 0;

    townElements.forEach((town) => {
      const townName = town.textContent.trim().toLowerCase();

      if (townName.includes(searchText) && searchText !== '') {
        town.style.fontWeight = 'bold';
        town.style.textDecoration = 'underline';
        matchCount++;
      }
    });

    return matchCount;
  }

  function showMatchCount(matchCount) {
    const resultElement = document.getElementById('result');
    resultElement.textContent = `${matchCount} matches found`;
  }
}
