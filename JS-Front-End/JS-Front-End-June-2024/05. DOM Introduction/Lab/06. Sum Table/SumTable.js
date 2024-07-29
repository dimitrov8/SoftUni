function sumTable() {
  document.getElementById('sum').textContent = '';
  const total = Array.from(
    document.querySelectorAll('table tr td:nth-child(even):not(#id)')
  )
    .reduce((sum, td) => sum + Number(td.textContent), 0)
    .toFixed(2);

  document.getElementById('sum').textContent = total;
}
