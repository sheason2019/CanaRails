export default function DataTable() {
  return (
    <div class="overflow-x-auto bg-base-200 mt-4 rounded-lg">
      <table class="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>实例名称</th>
            <th>状态</th>
            <th>镜像地址</th>
            <th>镜像</th>
            <th>映射端口</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <th>1</th>
            <td>Master</td>
            <td>Running</td>
            <td></td>
            <td>hello-world</td>
            <td>80</td>
            <td>
              {/** TODO: dropdown 详情、编辑、关闭 */}
              <button>操作</button>
            </td>
          </tr>
          <tr>
            <th>2</th>
            <td>Test</td>
            <td>Stop</td>
            <td></td>
            <td>hello-world</td>
            <td>80</td>
            <td>
              <button>详情</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}
