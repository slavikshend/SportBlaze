let password = '123098321890Or%';
function findThing(thing: string) {
  cy.get('body').then(($body) => {
    if ($body.find(`td:contains("${thing}")`).length > 0) {
      cy.log('Found');
    } else {
      cy.get('button[aria-label="Наступна"]').click({ force: true }).then(() => {
        cy.wait(500);
        findThing(thing);
      });
    }
  });
}
describe('admin functionality', () => {
  beforeEach(() => {
    cy.visit('http://localhost:4200');
    cy.get('.acc_circle').click({ force: true });
    cy.get('input[data-cy="email-cy"]').type('admin@example.com');
    cy.get('input[data-cy="password-cy"]').type(password);
    cy.get('.login-button').click({ force: true });
  })
  it('admin login', () => {
    cy.contains('Акційні пропозиції', { timeout: 10000 }).should('be.visible');
    cy.get('.acc_circle').click({ force: true });
    cy.get('#divUserName').should('have.text', 'Адміністратор');
  })
  it('admin makes an order', () => {
    cy.get('.product-card').contains('Tunturi FitCross 50i').parents('.product-card')
      .within(() => {
        cy.get('.add-to-cart-button').click({ force: true });
      })
    cy.get('.product-card').contains('Tunturi FitCross 50i').parents('.product-card')
      .within(() => {
        cy.get('.add-to-cart-button').click({ force: true });
      })
    cy.get('button').contains('Оформити замовлення').click({ force: true });
    cy.contains('Метод оплати').click({ force: true });
    cy.get('mat-option').contains('Оплата онлайн').click({ force: true });
    cy.contains('Метод доставки').click({ force: true });
    cy.get('span').contains('Нова Пошта').click({ force: true });
    cy.get('.order-btn').click({ force: true });
    cy.url().should('eq', 'http://localhost:4200/payment');
    cy.get('.acc_circle').click({ force: true });
    cy.contains('Особистий кабінет').click({ force: true });
    cy.contains('Мої замовлення').click({ force: true });
    cy.get('tr').last().within(() => {
      cy.get('mat-select').click({ force: true });
    });
    cy.get('mat-option[value="Скасовано клієнтом"]').click({ force: true });
  })
  it('admin opens profile', () => {
    cy.get('.acc_circle').click({ force: true });
    cy.get('div[routerlink="/cabinet"]').click({ force: true });
    cy.get('input[id="mat-input-0"]').should('have.value', 'Адміністратор');
  })
  it('admin manages brands', () => {
    cy.get('.acc_circle').click({ force: true });
    cy.contains('Особистий кабінет').click({ force: true });
    cy.contains('Бренди').click({ force: true });
    cy.get('button[mattooltip="Додати"]').click({ force: true });
    cy.get('input[placeholder="Назва бренду"]').clear().type('TESTBRAND');
    cy.contains('Зберегти').click({ force: true });
    cy.get('input[placeholder="Пошук"]').type('TESTBRAND');
    cy.get('td').contains('TESTBRAND').should('be.visible');
    findThing('TESTBRAND');
    cy.get('td').contains('TESTBRAND').parents('tr').within(() => {
      cy.get('button[mattooltip="Редагувати"]').click({ force: true });
    })
    cy.get('input[placeholder="Назва бренду"]').click({ force: true }).focused().clear().type('TESTBRAND1');
    cy.contains('Зберегти').click({ force: true });
    findThing('TESTBRAND1');
    cy.get('td').contains('TESTBRAND1').parents('tr').within(() => {
      cy.get('button[mattooltip="Видалити"]').click({ force: true });
    })
    cy.contains('Так').should('be.visible').click({ force: true });
    cy.wait(500);
  })
  it('admin manages categories', () => {
    cy.get('.acc_circle').click({ force: true });
    cy.contains('Особистий кабінет').click({ force: true });
    cy.contains('Категорії').click({ force: true });
    cy.get('button[mattooltip="Додати"]').click({ force: true });
    cy.get('input[placeholder="Назва категорії"]').type('TESTCATEGORY');
    cy.contains('Зберегти').click({ force: true });
    cy.get('input[placeholder="Пошук"]').type('TESTCATEGORY');
    cy.get('td').contains('TESTCATEGORY').should('be.visible');
    findThing('TESTCATEGORY');
    cy.get('td').contains('TESTCATEGORY').parents('tr').within(() => {
      cy.get('button[mattooltip="Редагувати"]').click({ force: true });
    })
    cy.get('input[placeholder="Назва категорії"]').click({ force: true }).focused().clear().type('TESTCATEGORY1');
    cy.contains('Зберегти').click({ force: true });
    findThing('TESTCATEGORY1');
    cy.get('td').contains('TESTCATEGORY1').parents('tr').within(() => {
      cy.get('button[mattooltip="Видалити"]').click({ force: true });
    })
    cy.contains('Так').should('be.visible').click({ force: true });
    cy.wait(500);
  })
  it('admin manages products', () => {
    cy.get('.acc_circle').click({ force: true });
    cy.contains('Особистий кабінет').click({ force: true });
    cy.contains('Бренди').click({ force: true });
    cy.contains('Товари').click({ force: true });
    cy.get('button[mattooltip="Додати"]').click({ force: true });
    cy.get('input[placeholder="Назва"]').type('TESTPRODUCT');
    cy.get('textarea[placeholder="Опис"]').type('DESCRIPTION');
    cy.get('input[placeholder="Ціна"]').type('100');
    cy.get('input[placeholder="Знижка"]').type('10');
    cy.get('.quantity-button').contains('+').click({ force: true });
    cy.contains('Оберіть бренд').click({ force: true });
    cy.contains('mat-grid-tile', 'Cobra Labs').click({ force: true });
    cy.contains('Оберіть підкатегорію').click({ force: true });
    cy.get('mat-grid-tile').contains('Амінокислоти').click({ force: true });
    cy.get('.add-characteristic-button').click({ force: true });
    cy.get('input[name="featureName_0"]').type('FEATURENAME1');
    cy.get('input[name="featureValue_0"]').type('FEATUREVALUE1');
    cy.contains('Зберегти').click({ force: true });
    cy.get('input[placeholder="Пошук"]').type('TESTPRODUCT');
    cy.get('td').contains('TESTPRODUCT').should('be.visible');
    cy.get('input[placeholder="Пошук"]').click({ force: true }).focused().clear();
    findThing('TESTPRODUCT');
    cy.get('td').contains('TESTPRODUCT').parents('tr').within(() => {
      cy.get('button[mattooltip="Редагувати"]').click({ force: true });
    })
    cy.get('input[name="productName"]').click({ force: true }).focused().clear().type('TESTPRODUCT1');
    cy.contains('Зберегти').click({ force: true });
    cy.wait(1000);
    findThing('TESTPRODUCT1');
    cy.get('td').contains('TESTPRODUCT1').parents('tr').within(() => {
      cy.get('button[mattooltip="Видалити"]').click({ force: true });
    })
    cy.contains('Так').should('be.visible').click({ force: true });
    cy.wait(500);
  })
  it('admin clears log', () => {
    cy.get('.acc_circle').click({ force: true });
    cy.contains('Особистий кабінет').click({ force: true });
    cy.contains('Лог магазину').click({ force: true });
    cy.get('.clear-button').click({ force: true });
    cy.wait(500);
    cy.get('.log-line').should('not.exist');
  })
  it('admin manages orders', () => {
    cy.get('.acc_circle').click({ force: true });
    cy.contains('Особистий кабінет').click({ force: true });
    cy.contains('Замовлення').click({ force: true });
    cy.get('input[placeholder="Пошук"]').type('111');
    cy.get('td').contains('111').should('exist');
    cy.get('td').contains('111').parents('tr').within(() => {
      cy.get('mat-select').click({ force: true });
    });
    cy.contains("Скасовано магазином").click({ force: true });
    cy.get('td').contains('111').parents('tr').within(() => {
      cy.get('mat-select').click({ force: true });
    });
    cy.contains("Нове замовлення").click({ force: true });
  })
  it('admin adds to favourites', () => {
    cy.contains('Bcaa Mega Caps 300 капсул').parents('.product-card').within(() => {
      cy.get('.favourite-icon').click({ force: true });
    })
    cy.get('.acc_circle').click({ force: true });
    cy.contains('Особистий кабінет').click({ force: true });
    cy.contains('Закладки').click({ force: true });
    cy.contains('Bcaa Mega Caps 300 капсул').should('exist');
    cy.contains('Bcaa Mega Caps 300 капсул').parents('.product-card').within(() => {
      cy.get('.favourite-icon').click({ force: true });
    })
    cy.reload();
    cy.contains('Bcaa Mega Caps 300 капсул').should('not.exist');
  })
  it('admin leaves a feedback', () => {
    cy.contains('Bcaa Mega Caps 300 капсул').parents('.product-card').within(() => {
      cy.get('img[alt="Bcaa Mega Caps 300 капсул"]').click({ force: true });
    })
    cy.get('ngb-rating span[style*="cursor"]').eq(3).click({ force: true });
    cy.get('textarea[placeholder="Напишіть ваш коментар"]').type('TESTCOMMENT');
    cy.get('.add-feedback-button').click({ force: true });
    cy.get('.feedback-comment').contains('TESTCOMMENT').should('exist');
  });
  it('admin browses catalogue', () => {
    cy.get('mat-icon').contains('menu').click({ force: true });
    cy.get('button').contains('Тренажери').click({ force: true });
    cy.get('li').contains('Орбітреки').click({ force: true });
    cy.get('.product-card').eq(0).within(() => {
      cy.get('.product-name').should('contain.text', 'Tunturi FitCross');
    });
  });
  it('admin searches', () => {
    cy.get('input[placeholder="Пошук..."]').type('Tunturi');
    cy.get('.product-card').eq(0).should('contain.text', 'Tunturi FitCross');
  });
});
