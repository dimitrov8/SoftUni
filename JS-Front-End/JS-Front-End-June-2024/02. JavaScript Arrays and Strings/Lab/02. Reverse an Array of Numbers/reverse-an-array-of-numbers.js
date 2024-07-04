function solve(number, inputArray) {
  let newArray = inputArray.slice(0, number).reverse();

  console.log(newArray.join(' '));
}

solve(3, [10, 20, 30, 40, 50]);
