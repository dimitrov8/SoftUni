function create(words) {
  const parentElement = document.getElementById('content');

  // Loop through each word
  words.forEach((word) => {
    // Create div
    const div = document.createElement('div');
    // Create paragraph
    const p = document.createElement('p');
    // Assign paragraph text content
    p.textContent = word;

    // Initially hide the paragraph using a class
    p.style.display = 'none';

    // Append the paragraph to the div
    div.appendChild(p);

    // Add event listener to div
    div.addEventListener('click', showParagraph);

    // Append the div to the parent element
    parentElement.appendChild(div);
  });

  // Show paragraph function
  function showParagraph(event) {
    const p = event.target.querySelector('p');
    p && (p.style.display = 'block');
  }
}
