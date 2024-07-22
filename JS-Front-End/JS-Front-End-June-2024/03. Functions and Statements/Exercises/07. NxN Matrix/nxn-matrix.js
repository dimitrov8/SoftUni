function solve(number) {
  const row = Array(number).fill(number).join(' ');

  for (let i = 0; i < number; i++) {
    console.log(row);
  }
}

solve(7);
