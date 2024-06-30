function areSameNumbers(number) {
  let numberToArray = number.toString().split('');
  let result = numberToArray.every((n) => n === numberToArray[0]);
  let sum = numberToArray.reduce((acc, digit) => acc + parseInt(digit), 0);

  console.log(result);
  console.log(sum);
}

areSameNumbers(2222222);
