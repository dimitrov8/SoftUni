function solve(inputString) {
  const result = inputString.split(/(?=[A-Z])/).join(', ');

  console.log(result);
}

solve('SplitMeIfYouCanHaHaYouCantOrYouCan');
