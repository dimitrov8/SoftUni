function solve() {
  const textParts = document
    .getElementById('text')
    .value.toLowerCase()
    .split(' ');
  const namingConvention = document.getElementById('naming-convention').value;
  const resultElement = document.getElementById('result');
  let result;

  if (namingConvention == 'Pascal Case') {
    result = textParts
      .map((part) => part[0].toUpperCase() + part.slice(1))
      .join('');
  } else if (namingConvention == 'Camel Case') {
    result = textParts
      .map((part, index) =>
        index === 0 ? part : part[0].toUpperCase() + part.slice(1)
      )
      .join('');
  } else {
    result = 'Error!';
  }

  resultElement.textContent = result;
}
