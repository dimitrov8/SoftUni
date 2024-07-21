function processNumbers(num1, num2, num3) {
  function sum(num1, num2) {
    return num1 + num2;
  }

  const finalResult = sum(num1, num2) - num3;

  console.log(finalResult);
}

processNumbers(23, 6, 10);
