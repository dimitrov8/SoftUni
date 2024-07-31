function solve() {
  const textAreaInputElement = document.getElementById('input');
  const text = textAreaInputElement.value;

  const sentences = text
    .split('.')
    .filter((sentence) => sentence.trim().length > 0)
    .map((sentance) => sentance.trim() + '.');

  const outputDiv = document.getElementById('output');

  while (outputDiv.firstChild) {
    outputDiv.removeChild(outputDiv.firstChild);
  }

  const paragraphCount = Math.ceil(sentences.length / 3);

  for (let i = 0; i < paragraphCount; i++) {
    const start = i * 3;
    const end = start + 3;
    const paragraphSentance = sentences.slice(start, end).join('.');

    const paragraph = document.createElement('p');
    paragraph.textContent = paragraphSentance;

    outputDiv.appendChild(paragraph);
  }
}
