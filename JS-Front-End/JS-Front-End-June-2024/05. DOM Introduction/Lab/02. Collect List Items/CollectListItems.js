function extractText() {
  const items = document.querySelectorAll('#items li');
  const resultText = Array.from(items, (item) => item.innerHTML).join('\n');
  document.getElementById('result').value = resultText;
}
