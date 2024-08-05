function solve() {
  initialize();

  function initialize() {
    const menuToElement = document.getElementById('selectMenuTo');
    addOptions(menuToElement);

    const buttonElement = document.querySelector('button');
    buttonElement.addEventListener('click', function () {
      const selectedOption = getToConvertOption(menuToElement);
      convert(selectedOption);
    });

    function addOptions(menuToElement) {
      const options = [
        {
          value: 'binary',
          text: 'binary',
        },
        {
          value: 'hexadecimal',
          text: 'hexadecimal',
        },
      ];

      options.forEach((optionData) => {
        const option = document.createElement('option');
        option.value = optionData.value;
        option.text = optionData.text;
        menuToElement.appendChild(option);
      });
    }

    function getToConvertOption(menuToElement) {
      return menuToElement.value;
    }
  }

  function convert(selectedOption) {
    const number = Number(document.getElementById('input').value);
    const resultElement = document.getElementById('result');

    if (isNaN(number)) {
      return;
    }

    if (selectedOption === 'binary') {
      resultElement.value = number.toString(2);
    } else if (selectedOption === 'hexadecimal')
      resultElement.value = number.toString(16).toUpperCase();
  }
}
