function solve(number) {
  let sum = number
    .toString()
    .split('')
    .reduce((acc, digit) => acc + parseInt(digit), 0);

  console.log(sum);
}

solve(543);
