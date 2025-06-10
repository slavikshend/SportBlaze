let password1 = '123098321890Or%';

describe('authorized-client-tests', () => {
  beforeEach(() => {
    cy.visit('http://localhost:4200');
    cy.get('.acc_circle').click();
    cy.get('input[formcontrolname="email"]').type('hello@gmail.com');
    cy.get('input[formcontrolname=password]').type(password1);
    cy.get('.login-button').click();
  });

  it('client login', () => {
    cy.contains('Акційні пропозиції', { timeout: 10000 }).should('be.visible');
    cy.get('.acc_circle').click();
    cy.get('#divUserName').should('have.text', 'aa');
  });

  it('client makes an order', () => {
    cy.get('.product-card').contains('Tunturi FitCross 50i').parents('.product-card')
      .within(() => {
        cy.get('.add-to-cart-button').click();
      });
    cy.get('.product-card').contains('Tunturi FitCross 50i').parents('.product-card')
      .within(() => {
        cy.get('.add-to-cart-button').click();
      });
    cy.get('button').contains('Оформити замовлення').click();
    cy.contains('Метод оплати').click({ force: true });
    cy.get('mat-option').contains('Оплата онлайн').click();
    cy.contains('Метод доставки').click({ force: true });
    cy.get('mat-option').contains('Нова Пошта').click();
    cy.get('input[formcontrolname="city"]').type('Одеса');
    cy.get('input[formcontrolname="address"]').type('вул. Канатна, 13в');
    cy.get('.order-btn').click();
    cy.url().should('eq', 'http://localhost:4200/payment');
    cy.get('.acc_circle').click();
    cy.contains('Особистий кабінет').click();
    cy.contains('Мої замовлення').click();
    cy.get('tr').last().within(() => {
      cy.get('mat-select').click();
    });
    cy.get('mat-option[value="Скасовано клієнтом"]').click();
  });

  it('client opens profile', () => {
    cy.get('.acc_circle').click();
    cy.get('div[routerlink="/cabinet"]').click();
    cy.get('input[id="mat-input-0"]').should('have.value', 'aa');
  });

  it('client adds to favourites', () => {
    cy.contains('Bcaa Mega Caps 300 капсул').parents('.product-card').within(() => {
      cy.get('.favourite-icon').click();
    });
    cy.get('.acc_circle').click();
    cy.contains('Особистий кабінет').click();
    cy.contains('Закладки').click();
    cy.contains('Bcaa Mega Caps 300 капсул').should('exist');
    cy.contains('Bcaa Mega Caps 300 капсул').parents('.product-card').within(() => {
      cy.get('.favourite-icon').click();
    });
    cy.reload();
    cy.contains('Bcaa Mega Caps 300 капсул').should('not.exist');
  });

  it('client leaves a feedback', () => {
    cy.contains('Bcaa Mega Caps 300 капсул').parents('.product-card').within(() => {
      cy.get('img[alt="Bcaa Mega Caps 300 капсул"]').click();
    });
    cy.get('ngb-rating span[style*="cursor"]').eq(3).click();
    cy.get('textarea[placeholder="Напишіть ваш коментар"]').type('TESTCOMMENT');
    cy.get('.add-feedback-button').click();
    cy.get('.feedback-comment').contains('TESTCOMMENT').should('exist');
  });
  it('client browses catalogue', () => {
    cy.get('mat-icon').contains('menu').click();
    cy.get('button').contains('Тренажери').click();
    cy.get('li').contains('Орбітреки').click();
    cy.get('.product-card').eq(0).within(() => {
      cy.get('.product-name').should('contain.text', 'Tunturi FitCross');
    });
  });
  it('client searches', () => {
    cy.get('input[placeholder="Пошук..."]').type('Tunturi');
    cy.get('.product-card').eq(0).should('contain.text', 'Tunturi FitCross');
  });
});
