function solve(numOne, numTwo, operator) {
  const operations = {
    add: (a, b) => a + b,
    subtract: (a, b) => a - b,
    divide: (a, b) => a / b,
    multiply: (a, b) => a * b,
  };

  const operationFunc = operations[operator];

  if (operationFunc) {
    console.log(operationFunc(numOne, numTwo));
  } else {
    console.log('Invalid operator');
  }
}

solve(40, 8, 'add');
