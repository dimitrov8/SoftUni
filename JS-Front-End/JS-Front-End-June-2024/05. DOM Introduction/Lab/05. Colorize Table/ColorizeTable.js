function colorize() {
  const evenRows = document.querySelectorAll('table tr:nth-child(even)');
  evenRows.forEach((er) => {
    er.style.backgroundColor = 'teal';
  });
}
