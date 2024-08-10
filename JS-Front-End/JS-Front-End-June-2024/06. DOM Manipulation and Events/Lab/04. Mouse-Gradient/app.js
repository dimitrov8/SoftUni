function attachGradientEvents() {
  const gradientElement = document.getElementById('gradient');
  const resultElement = document.getElementById('result');

  gradientElement.addEventListener('mousemove', onMouseMove);
  gradientElement.addEventListener('mouseout', onMouseOut);

  function onMouseMove(event) {
    const currentMouseX = event.offsetX;
    const percentage = Math.floor((currentMouseX / this.clientWidth) * 100);
    resultElement.textContent = percentage + '%';
  }

  function onMouseOut(event) {
    resultElement.textContent = '';
  }
}
