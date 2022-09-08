output "aws_acm_certificate_validation_vpn_server_arn" {
  value = aws_acm_certificate_validation.vpn_server.certificate_arn
}

output "aws_acm_certificate_vpn_client_root_arn" {
    value = aws_acm_certificate.vpn_client_root.arn
}