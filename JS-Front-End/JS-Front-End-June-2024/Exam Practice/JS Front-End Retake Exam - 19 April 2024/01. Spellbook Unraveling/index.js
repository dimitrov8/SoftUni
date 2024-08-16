function solve(input) {
  const originalEncodedSpell = input.shift();
  let decodedSpell = originalEncodedSpell;

  let args = input.shift();
  while (args !== 'End') {
    const command = args;

    if (isValidCommand(command)) {
      let success = true;

      if (command === 'RemoveEven') {
        handleRemoveEven();
      } else if (command.startsWith('TakePart')) {
        handleTakePart(command);
      } else if (command.startsWith('Reverse')) {
        success = handleReverse(command);
      }

      if (success) {
        console.log(decodedSpell);
      }
    }

    args = input.shift();
  }
  console.log(`The concealed spell is: ${decodedSpell}`);

  function handleRemoveEven() {
    decodedSpell = Array.from(decodedSpell)
      .filter((_, i) => i % 2 === 0)
      .join('');
  }

  function handleTakePart(command) {
    const [, startIndex, endIndex] = command.split('!').map(Number);

    decodedSpell = decodedSpell.slice(startIndex, endIndex);
  }

  function handleReverse(command) {
    const substring = command.split('!')[1];
    if (decodedSpell.includes(substring)) {
      const reversedSubstring = substring.split('').reverse().join('');

      const startIndex = decodedSpell.indexOf(substring);

      decodedSpell =
        decodedSpell.slice(0, startIndex) +
        decodedSpell.slice(startIndex + substring.length) +
        reversedSubstring;

      return true;
    } else {
      console.log('Error');

      return false;
    }
  }

  function isValidCommand(command) {
    return (
      command === 'RemoveEven' ||
      command.startsWith('TakePart') ||
      command.startsWith('Reverse')
    );
  }
}

solve([
  'hZwemtroiui5tfone1haGnanbvcaploL2u2a2n2i2m',
  'TakePart!31!42',
  'RemoveEven',
  'Reverse!anim',
  'Reverse!sad',
  'End',
]);
