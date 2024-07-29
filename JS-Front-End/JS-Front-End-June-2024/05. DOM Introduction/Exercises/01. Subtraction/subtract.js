function subtract() {
  const num1Value = Number(document.getElementById('firstNumber').value);
  const num2Value = Number(document.getElementById('secondNumber').value);

  document.getElementById('result').textContent = num1Value - num2Value;
}
