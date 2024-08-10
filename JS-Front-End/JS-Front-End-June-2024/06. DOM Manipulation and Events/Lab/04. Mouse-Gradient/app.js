function attachGradientEvents() {
  let gradientElement = document.getElementById('gradient');
  let resultElement = document.getElementById('result');

  gradientElement.addEventListener('mousemove', onMouseMove);
  gradientElement.addEventListener('mouseout', onMouseOut);

  function onMouseMove(event) {
    let currentMouseX = event.offsetX;
    let percentage = Math.floor((currentMouseX / this.clientWidth) * 100);
    resultElement.textContent = percentage + '%';
  }

  function onMouseOut(event) {
    resultElement.textContent = '';
  }
}
