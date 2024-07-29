function editElement(element, match, replacer) {
  if (element && element.textContent) {
    const regex = new RegExp(match, 'g');

    element.textContent = element.textContent.replace(regex, replacer);
  }
}
