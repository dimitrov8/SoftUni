function solve(number) {
  let oddSum = 0;
  let evenSum = 0;

  number
    .toString()
    .split('')
    .forEach((digitChar) => {
      const digit = parseInt(digitChar, 10);
      if (digit % 2 == 0) {
        evenSum += digit;
      } else {
        oddSum += digit;
      }
    });

  console.log(`Odd sum = ${oddSum}, Even sum = ${evenSum}`);
}

solve(1000435);
