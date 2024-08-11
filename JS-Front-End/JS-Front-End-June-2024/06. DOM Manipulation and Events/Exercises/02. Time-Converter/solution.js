function attachEventsListeners() {
  // Map time units to their corresponding input elements by ID
  const timeElements = {
    days: document.getElementById('days'),
    hours: document.getElementById('hours'),
    minutes: document.getElementById('minutes'),
    seconds: document.getElementById('seconds'),
  };

  // Select all input buttons with type="button" and value="Convert"
  const convertButtonElements = document.querySelectorAll(
    'input[type="button"][value="Convert"]'
  );

  // Add a click event listener to each "Convert" button
  convertButtonElements.forEach((convertBtnEl) =>
    convertBtnEl.addEventListener('click', convert)
  );

  // Convert function to handle the conversion based on input value and time unit
  function convert(event) {
    // Find the closest parent div that contains the clicked button
    const div = event.target.closest('div');

    // Get the 'for' attribute value from the label inside this div
    const labelFor = div.querySelector('label').getAttribute('for');

    // Retrieve and parse the input value from the text input field
    const inputValue = Number(div.querySelector('input[type="text"]').value);

    // Check if the input value is a positive number
    if (inputValue > 0) {
      // Define conversion factors for each time unit
      const conversionFactors = {
        days: { hours: 24, minutes: 1440, seconds: 86400 },
        hours: { days: 1 / 24, minutes: 60, seconds: 3600 },
        minutes: { days: 1 / 1440, hours: 1 / 60, seconds: 60 },
        seconds: { days: 1 / 86400, hours: 1 / 3600, minutes: 1 / 60 },
      };

      // Get the appropriate conversion factors based on the 'for' attribute
      const factors = conversionFactors[labelFor];
      for (const [unit, factor] of Object.entries(factors)) {
        timeElements[unit].value = inputValue * factor;
      }
    }
  }
}
