package ui;

import services.ProductService;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class MainMenu{
    private ProductService productService = new ProductService();

    public void showGUI() {
        // Create the main frame
        JFrame frame = new JFrame("Hardware Store Management");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(400, 300);
        frame.setLayout(new BorderLayout());

        // Title
        JLabel title = new JLabel("Hardware Store Management", JLabel.CENTER);
        title.setFont(new Font("Arial", Font.BOLD, 16));
        frame.add(title, BorderLayout.NORTH);

        // Buttons Panel
        JPanel buttonPanel = new JPanel();
        buttonPanel.setLayout(new GridLayout(2, 1, 10, 10));

        JButton addProductButton = new JButton("Add Product");
        JButton exitButton = new JButton("Exit");

        buttonPanel.add(addProductButton);
        buttonPanel.add(exitButton);

        frame.add(buttonPanel, BorderLayout.CENTER);

        // Add Action Listeners
        addProductButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                openAddProductForm();
            }
        });

        exitButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                System.exit(0);
            }
        });

        // Show the frame
        frame.setVisible(true);
    }

    private void openAddProductForm() {
        JFrame addProductFrame = new JFrame("Add Product");
        addProductFrame.setSize(400, 300);
        addProductFrame.setLayout(new GridLayout(5, 2, 10, 10));

        JLabel nameLabel = new JLabel("Product Name:");
        JTextField nameField = new JTextField();

        JLabel categoryLabel = new JLabel("Category:");
        JTextField categoryField = new JTextField();

        JLabel priceLabel = new JLabel("Price:");
        JTextField priceField = new JTextField();

        JLabel quantityLabel = new JLabel("Quantity:");
        JTextField quantityField = new JTextField();

        JButton submitButton = new JButton("Add Product");

        addProductFrame.add(nameLabel);
        addProductFrame.add(nameField);
        addProductFrame.add(categoryLabel);
        addProductFrame.add(categoryField);
        addProductFrame.add(priceLabel);
        addProductFrame.add(priceField);
        addProductFrame.add(quantityLabel);
        addProductFrame.add(quantityField);
        addProductFrame.add(new JLabel()); // Empty cell for alignment
        addProductFrame.add(submitButton);

        submitButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                String name = nameField.getText();
                String category = categoryField.getText();
                double price = Double.parseDouble(priceField.getText());
                int quantity = Integer.parseInt(quantityField.getText());

                boolean success = productService.addProduct(name, category, price, quantity);
                JOptionPane.showMessageDialog(addProductFrame, success ? "Product added successfully!" : "Failed to add product.");
            }
        });

        addProductFrame.setVisible(true);
    }

    public static void main(String[] args) {
        new MainMenu().showGUI();
    }
}
