function focused() {
  const textInputElements = document.querySelectorAll(
    'div > input[type="text"]'
  );

  textInputElements.forEach((input) => {
    input.addEventListener('focus', toggleFocus);
    input.addEventListener('blur', toggleFocus);
  });

  function toggleFocus(event) {
    const parentDiv = event.target.closest('div');
    parentDiv?.classList.toggle('focused', event.type === 'focus');
  }
}
