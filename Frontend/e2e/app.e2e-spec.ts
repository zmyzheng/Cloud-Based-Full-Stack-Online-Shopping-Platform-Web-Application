import { MaasProjectPage } from './app.po';

describe('maas-project App', function() {
  let page: MaasProjectPage;

  beforeEach(() => {
    page = new MaasProjectPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
