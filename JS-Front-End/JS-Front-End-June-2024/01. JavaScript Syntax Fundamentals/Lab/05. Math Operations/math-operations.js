function solve(num1, num2, arg) {
  let result;
  switch (arg) {
    case '+':
      result = num1 + num2;
      break;
    case '-':
      result = num1 - num2;
      break;
    case '*':
      result = num1 * num2;
      break;
    case '/':
      result = num1 / num2;
      break;
    case '%':
      result = num1 % num2;
      break;
    case '**':
      result = num1 ** num2;
      break;
    default:
      result = null;
      break;
  }

  console.log(result);
}

solve(2, 2, '+');
