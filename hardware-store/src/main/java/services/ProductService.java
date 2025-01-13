package services;

import dao.ProductDAO;
import models.Product;

public class ProductService {
    private ProductDAO productDAO = new ProductDAO();

    public boolean addProduct(String name, String category, double price, int quantity) {
        if (price <= 0 || quantity < 0) {
            System.out.println("Invalid price or quantity!");
            return false;
        }
        Product product = new Product(0, name, category, price, quantity);
        return productDAO.addProduct(product);
    }
}
