function calc() {
  const num1ElValue = Number(document.getElementById('num1').value);
  const num2ElValue = Number(document.getElementById('num2').value);
  if (!isNaN(num1ElValue) && !isNaN(num2ElValue)) {
    document.getElementById('sum').value = num1ElValue + num2ElValue;
  } else {
    document.getElementById('sum').value = 'Invalid input';
  }
}
