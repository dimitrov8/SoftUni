function solve(num1, num2) {
  let numbersArray = new Array();
  for (let index = num1; index <= num2; index++) {
    let currentNumber = index;
    numbersArray.push(currentNumber);
  }

  let sum = numbersArray.reduce((a, b) => a + b, 0);

  console.log(numbersArray.join(' '));
  console.log(`Sum: ${sum}`);
}

solve(5, 10);
