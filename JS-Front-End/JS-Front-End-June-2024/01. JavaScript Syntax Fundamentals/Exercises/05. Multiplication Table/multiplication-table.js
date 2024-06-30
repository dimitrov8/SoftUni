function solve(number) {
  for (let index = 1; index <= 10; index++) {
    let times = index;
    let sum = number * times;
    console.log(`${number} X ${times} = ${sum}`);
  }
}

solve(5);
