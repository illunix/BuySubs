output "aws_security_group_allow_all_id" {
  value = aws_security_group.allow_all.id
}

output "aws_subnet_main_id" {
  value = aws_subnet.main.id
}

output "aws_subnet_second_id" {
  value = aws_subnet.second.id
}
