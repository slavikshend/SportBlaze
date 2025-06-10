describe('unauthorized-client-tests', () => {
  const testEmail = `testuser_${Date.now()}@example.com`;
  const testPassword = '123098321890Or%';
  beforeEach(() => {
    cy.visit('http://localhost:4200');
  });
  it('unauthrized user registers', () => {
    cy.get('.acc_circle').click();
    cy.get('button').contains('Реєстрація').click();
    cy.get('input[formcontrolname="firstName"]').type('TESTUSER');
    cy.get('input[formcontrolname="email"]').type(testEmail);
    cy.get('input[formcontrolname="phoneNumber"]').type('TESTPHONE', { force: true });
    cy.get('input[formcontrolname="lastName"]').type('TESTUSER');
    cy.get('input[formcontrolname="password"]').type(testPassword);
    cy.get('input[formcontrolname="confirmPassword"]').type(testPassword);
    cy.contains('Зареєструватися').click();
    cy.get('input[data-cy="email-cy"]').type(testEmail);
    cy.get('input[data-cy="password-cy"]').type(testPassword);
    cy.wait(1000);
    cy.get('.login-button').click();
    cy.get('.acc_circle').click();
    cy.get('#divUserName').should('have.text', 'TESTUSER');
  })
  it('unathorized client makes an order', () => {
    cy.get('.product-card').contains('Tunturi FitCross 50i').parents('.product-card')
      .within(() => {
        cy.get('.add-to-cart-button').click();
      });
    cy.get('button').contains('Оформити замовлення').click();
    cy.contains('Метод оплати').click({force: true});
    cy.get('mat-option').contains('Оплата онлайн').click();
    cy.contains('Метод доставки').click({ force: true });
    cy.get('mat-option').contains('Нова Пошта').click();
    cy.get('input[formcontrolname="firstName"]').type('TESTUSER');
    cy.get('input[formcontrolname="email"]').type(testEmail, {force: true});
    cy.get('input[formcontrolname="phone"]').type('TESTPHONE', { force: true });
    cy.get('input[formcontrolname="lastName"]').type('TESTUSER', { force: true });
    cy.get('input[formcontrolname="city"]').type('Одеса');
    cy.get('input[formcontrolname="address"]').type('вул. Канатна, 13в');
    cy.get('.order-btn').click();
    cy.url().should('eq', 'http://localhost:4200/payment');
  });
  it('unauthorized-user browses catalogue', () => {
    cy.get('mat-icon').contains('menu').click();
    cy.get('button').contains('Тренажери').click();
    cy.get('li').contains('Орбітреки').click();
    cy.get('.product-card').eq(0).within(() => {
      cy.get('.product-name').should('contain.text', 'Tunturi FitCross');
    });
  });
  it('unauthorized-user searches', () => {
    cy.get('input[placeholder="Пошук..."]').type('Tunturi');
    cy.get('.product-card').eq(0).should('contain.text', 'Tunturi FitCross');
  });
});
