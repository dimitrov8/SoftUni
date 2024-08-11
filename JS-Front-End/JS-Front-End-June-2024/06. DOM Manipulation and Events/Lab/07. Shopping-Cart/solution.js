function solve() {
  const buttonElements = document.querySelectorAll('.product .add-product');
  const textArea = document.querySelector('textarea');
  const checkOutElement = document.querySelector('.checkout');
  let boughtProducts = {};

  addEvents();

  function addEvents() {
    buttonElements.forEach((btnEl) => {
      btnEl.addEventListener('click', addToCart);
    });

    checkOutElement.addEventListener('click', checkOut);
  }

  function addToCart(event) {
    const productInfo = event.target.closest('.product');
    const name = productInfo.querySelector('.product-title').textContent.trim();
    const priceElement = productInfo.querySelector('.product-line-price');
    const price = Number(priceElement.textContent);

    if (boughtProducts[name]) {
      boughtProducts[name] += price;
    } else {
      boughtProducts[name] = price;
    }

    textArea.value += `Added ${name} for ${price.toFixed(2)} to the cart.\n`;
  }

  function checkOut(event) {
    const boughtProductNames = Object.keys(boughtProducts).join(', ');

    const totalPrice = Object.values(boughtProducts)
      .reduce((sum, price) => sum + price, 0)
      .toFixed(2);

    buttonElements.forEach((btnEl) => (btnEl.disabled = true));
    checkOutElement.disabled = true;

    textArea.value += `You bought ${boughtProductNames} for ${totalPrice}.`;
  }
}
