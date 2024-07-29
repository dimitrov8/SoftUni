function extract(content) {
  const element = document.getElementById(content);
  const pattern = /\(([^)]+)\)/g;

  const matches = element.textContent.match(pattern);

  if (matches) {
    return matches.map((match) => match.slice(1, -1)).join('; ');
  } else {
    return 'No matches found.';
  }
}
