function solve(numbers) {
  numbers.forEach((number) => {
    let isPalindrome = false;
    const stringNumber = number.toString();
    const reversedStringNumber = stringNumber.split('').reverse().join('');

    if (stringNumber === reversedStringNumber) {
      isPalindrome = true;
    }

    console.log(isPalindrome);
  });
}

solve([123, 323, 421, 121]);
