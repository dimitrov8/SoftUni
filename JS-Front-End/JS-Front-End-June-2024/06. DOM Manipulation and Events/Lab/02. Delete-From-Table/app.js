function deleteByEmail() {
  const emailInputElement = document.querySelector(
    'input[type="text"][name="email"]'
  );
  const emailToDelete = document
    .getElementsByName('email')[0]
    .value.trim()
    .toLowerCase();
  const resultElement = document.getElementById('result');
  resultElement.textContent = 'Not found.';

  const rows = Array.from(document.querySelectorAll('tbody tr'));

  for (let i = 0; i < rows.length; i++) {
    const row = rows[i];
    const emailCellText = row
      .querySelector('td:nth-child(2)')
      .textContent.trim()
      .toLowerCase();

    if (emailCellText == emailToDelete) {
      row.remove();
      resultElement.textContent = 'Deleted.';
      break;
    }
  }
}
