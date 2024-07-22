function divideFactorials(number1, number2) {
  function factorial(number) {
    if (number === 0) {
      return 1;
    }

    let result = 1;

    for (let i = number; i > 1; i--) {
      result *= i;
    }

    return result;
  }

  if (number1 < 0 || number2 < 0) {
    console.log('Factorials are not defined for negative numbers.');
    return;
  }

  let number1Factorial = factorial(number1);
  let number2Factorial = factorial(number2);

  if (number2Factorial === 0) {
    console.log('Division by zero is not allowed.');
    return;
  }

  const result = number1Factorial / number2Factorial;

  console.log(result.toFixed(2));
}

divideFactorials(5, 2);
