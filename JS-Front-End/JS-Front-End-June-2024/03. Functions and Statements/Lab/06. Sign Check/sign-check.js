function solve(numOne, numTwo, numThree) {
  let negativeCount = 0;
  if (numOne < 0) negativeCount++;
  if (numTwo < 0) negativeCount++;
  if (numThree < 0) negativeCount++;

  if (negativeCount % 2 === 0) {
    console.log('Positive');
  } else {
    console.log('Negative');
  }
}

solve(5, 12, -15);
