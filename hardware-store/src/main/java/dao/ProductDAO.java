package dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import utils.DatabaseConnection;
import models.Product;

public class ProductDAO {
    public boolean addProduct(Product product) {
        String query = "INSERT INTO products (name, category, price, quantity_in_stock) VALUES (?, ?, ?, ?)";
        try (Connection conn = DatabaseConnection.getConnection();
             PreparedStatement stmt = conn.prepareStatement(query)) {
            stmt.setString(1, product.getName());
            stmt.setString(2, product.getCategory());
            stmt.setDouble(3, product.getPrice());
            stmt.setInt(4, product.getQuantityInStock());
            return stmt.executeUpdate() > 0;
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }
}
