function solve(char1, char2) {
  const char1AsciiCode = char1.charCodeAt(0);
  const char2AsciiCode = char2.charCodeAt(0);

  const start = Math.min(char1AsciiCode, char2AsciiCode);
  const end = Math.max(char1AsciiCode, char2AsciiCode);

  let characters = '';

  for (let i = start + 1; i < end; i++) {
    characters += String.fromCharCode(i) + ' ';
  }

  console.log(characters);
}

solve('a', 'd');
