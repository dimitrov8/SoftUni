function solve(inputArray) {
  let evenSum = 0;
  let oddSum = 0;

  inputArray.forEach((element) => {
    if (element % 2 == 0) {
      evenSum += element;
    } else {
      oddSum += element;
    }
  });

  let difference = evenSum - oddSum;

  console.log(difference);
}

solve([1, 2, 3, 4, 5, 6]);
