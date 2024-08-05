function generateReport() {
  const checkedHeaders = Array.from(
    document.querySelectorAll('th input[type="checkbox"]:checked')
  ).map((checkBox) => {
    const headerCell = checkBox.parentElement;
    const allHeaders = Array.from(document.querySelectorAll('thead th'));
    const headerIndex = allHeaders.indexOf(headerCell);

    return {
      index: headerIndex,
      name: checkBox.name,
    };
  });

  const reportData = Array.from(document.querySelectorAll('tbody tr')).map(
    (row) => {
      const rowData = {};

      checkedHeaders.forEach(({ index, name }) => {
        const cell = row.children[index];
        rowData[name] = cell ? cell.textContent.trim() : '';
      });

      return rowData;
    }
  );

  document.getElementById('output').value = JSON.stringify(reportData, null, 2);
}
