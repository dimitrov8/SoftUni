function getSmallestNumber(numberOne, numberTwo, numberThree) {
  let smallest = numberOne;

  if (numberTwo < smallest) {
    smallest = numberTwo;
  }

  if (numberThree < smallest) {
    smallest = numberThree;
  }

  console.log(smallest);
}

getSmallestNumber(2, 5, 3);
